using System;
using System.ComponentModel.DataAnnotations;

namespace FlynnNotesBlog.Enums
{
    public enum ReadyStatus
    {
        [Display(Name = "Incomplete")]
        Incomplete,
        [Display(Name = "Production Ready")]
        ProductionReady,
        [Display(Name = "Preview Ready")]
        PreviewReady
    }
}
