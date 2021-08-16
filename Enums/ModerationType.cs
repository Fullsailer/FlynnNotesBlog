using System;
using System.ComponentModel;

namespace FlynnNotesBlog.Enums
{
    public enum ModerationType
    {
        [Description("Political Propaganda")]
       Political,
        [Description("Offensive Language")]
        Language,
        [Description("Drug References")]
        Drugs,
        [Description("Threating Speech")]
        Threating,
        [Description("Sexual Content")]
        Sexual,
        [Description("Hate Speech")]
        HateSpeech,
        [Description("Targeted Shaming")]
        Shaming
    }
}
