// <copyright file="CameraUpdateEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Physics.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Components;
using FinalEngine.Input.Controllers;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Physics.Components;

[EntitySystemProcess(EventName = "Update")]
public sealed class CameraUpdateEntitySystem : EntitySystemBase
{
    private readonly IGameController controller;

    private readonly IKeyboard keyboard;

    private readonly IMouse mouse;

    public CameraUpdateEntitySystem(IKeyboard keyboard, IMouse mouse, IGameController controller)
    {
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
        this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() && entity.ContainsComponent<VelocityComponent>() && entity.ContainsComponent<CameraComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var velocity = entity.GetComponent<VelocityComponent>();
            var camera = entity.GetComponent<CameraComponent>();

            if (!camera.IsEnabled)
            {
                continue;
            }

            this.HandleMovement(transform, velocity);
            this.HandleRotation(transform, velocity, camera);
        }
    }

    private void HandleMovement(TransformComponent transform, VelocityComponent velocity)
    {
        float moveAmount = velocity.Speed;

        float deadzone = 0.4f;

        float axisX = this.controller.GetAxis(0, ControllerAxis.LeftX);
        float axisY = this.controller.GetAxis(0, ControllerAxis.LeftY);

        // Apply per-axis deadzone
        axisX = Math.Abs(axisX) < deadzone ? 0 : axisX;
        axisY = Math.Abs(axisY) < deadzone ? 0 : axisY;

        var movement = new Vector3(-axisX, 0, -axisY);

        if (movement.Length() > 1f)
        {
            movement = Vector3.Normalize(movement);
        }

        if (this.keyboard.IsKeyDown(Key.W) || movement.Z > 0)
        {
            transform.Translate(transform.Forward, moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.S) || movement.Z < 0)
        {
            transform.Translate(transform.Forward, -moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.A) || movement.X > 0)
        {
            transform.Translate(transform.Left, -moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.D) || movement.X < 0)
        {
            transform.Translate(transform.Left, moveAmount);
        }
    }

    private void HandleRotation(TransformComponent transform, VelocityComponent velocity, CameraComponent camera)
    {
        var viewport = camera.Viewport;
        var centerPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

        float rotateSpeed = Math.Clamp(velocity.Speed, 0, 2);

        if (this.keyboard.IsKeyReleased(Key.Escape))
        {
            camera.IsLocked = false;
        }

        if (this.mouse.IsButtonReleased(MouseButton.Left))
        {
            this.mouse.Location = new PointF(centerPosition.X, centerPosition.Y);
            camera.IsLocked = true;
        }

        if (this.controller.IsButtonReleased(0, ControllerButton.A))
        {
            camera.IsLocked = true;
        }

        if (camera.IsLocked)
        {
            float axisX = this.controller.GetAxis(0, ControllerAxis.RightX);
            float axisY = this.controller.GetAxis(0, ControllerAxis.RightY);

            float deadzone = 0.2f;
            bool useController = new Vector2(axisX, axisY).Length() > deadzone;

            var deltaPosition = new Vector2(
                this.mouse.Location.X - centerPosition.X,
                this.mouse.Location.Y - centerPosition.Y);

            if (useController)
            {
                deltaPosition = new Vector2(axisX, axisY);
            }

            bool canRotateX = deltaPosition.X != 0;
            bool canRotateY = deltaPosition.Y != 0;

            if (canRotateX)
            {
                transform.Rotate(
                    transform.Left,
                    -MathHelper.DegreesToRadians(deltaPosition.Y * rotateSpeed)
                );
            }

            if (canRotateY)
            {
                transform.Rotate(
                    Vector3.UnitY,
                    -MathHelper.DegreesToRadians(deltaPosition.X * rotateSpeed)
                );
            }

            if (!useController && (canRotateX || canRotateY))
            {
                this.mouse.Location = new PointF(centerPosition.X, centerPosition.Y);
            }
        }
    }
}
