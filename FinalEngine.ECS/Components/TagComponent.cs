// <copyright file="TagComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components;

using System.ComponentModel;

/// <summary>
/// Represents a tag for an <see cref="Entity"/>.
/// </summary>
///
/// <remarks>
/// An <see cref="Entity"/> can be associated with a tag for identification. However, an <see cref="Entity"/> also has a unique identifier - see <see cref="Entity.UniqueIdentifier"/>.
/// </remarks>
///
/// <seealso cref="IEntityComponent" />
/// <seealso cref="INotifyPropertyChanged" />
[Category("Core")]
public sealed class TagComponent : IEntityComponent, INotifyPropertyChanged
{
    private string? name;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the name (or tag).
    /// </summary>
    ///
    /// <value>
    /// The name (or tag).
    /// </value>
    public string? Name
    {
        get
        {
            return this.name;
        }

        set
        {
            if (this.name == value)
            {
                return;
            }

            this.name = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
        }
    }
}
