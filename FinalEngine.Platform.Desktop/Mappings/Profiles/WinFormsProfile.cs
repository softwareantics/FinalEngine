// <copyright file="WinFormsProfile.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Mappings.Profiles;

using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Platform;

internal sealed class WinFormsProfile : Profile
{
    public WinFormsProfile()
    {
        //// WindowStyle -> FormBorderStyle
        this.CreateMap<WindowStyle, FormBorderStyle>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(WindowStyle.Fixed, FormBorderStyle.FixedSingle)
                 .MapValue(WindowStyle.Resizable, FormBorderStyle.Sizable)
                 .MapValue(WindowStyle.Borderless, FormBorderStyle.None);
            });

        //// FormBorderStyle -> WindowStyle
        this.CreateMap<FormBorderStyle, WindowStyle>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(FormBorderStyle.FixedSingle, WindowStyle.Fixed)
                 .MapValue(FormBorderStyle.Sizable, WindowStyle.Resizable)
                 .MapValue(FormBorderStyle.None, WindowStyle.Borderless);
            });

        //// WindowState -> FormWindowState
        this.CreateMap<WindowState, FormWindowState>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(WindowState.Normal, FormWindowState.Normal)
                 .MapValue(WindowState.Minimized, FormWindowState.Minimized)
                 .MapValue(WindowState.Maximized, FormWindowState.Maximized)
                 .MapValue(WindowState.Fullscreen, FormWindowState.Maximized);
            });

        //// FormWindowState -> WindowState
        this.CreateMap<FormWindowState, WindowState>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(FormWindowState.Normal, WindowState.Normal)
                 .MapValue(FormWindowState.Minimized, WindowState.Minimized)
                 .MapValue(FormWindowState.Maximized, WindowState.Maximized);
            });
    }
}
