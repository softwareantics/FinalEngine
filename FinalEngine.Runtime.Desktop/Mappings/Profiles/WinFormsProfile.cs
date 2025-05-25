// <copyright file="WinFormsProfile.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Desktop.Mappings.Profiles;

using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Platform;

internal sealed class WinFormsProfile : Profile
{
    public WinFormsProfile()
    {
        this.CreateMap<WindowStyle, FormBorderStyle>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(WindowStyle.Fixed, FormBorderStyle.FixedSingle)
                 .MapValue(WindowStyle.Resizable, FormBorderStyle.Sizable)
                 .MapValue(WindowStyle.Borderless, FormBorderStyle.None);
            })
            .ReverseMap();

        this.CreateMap<WindowState, FormWindowState>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(WindowState.Normal, FormWindowState.Normal)
                 .MapValue(WindowState.Minimized, FormWindowState.Minimized)
                 .MapValue(WindowState.Maximized, FormWindowState.Maximized)
                 .MapValue(WindowState.Fullscreen, FormWindowState.Maximized);
            })
            .ReverseMap();
    }
}
