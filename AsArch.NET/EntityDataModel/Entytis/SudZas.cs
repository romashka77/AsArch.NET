﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AsArch.NET.EntityDataModel.Entytis
{
    //[Serializable]
    public class SudZas : BaseOrder
    {
        public string N { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime? DateValue { get; set; }
        public string DateValue { get; set; }
        public string TimeValue { get; set; }
        public string Comment { get; set; }
        public string Isp { get; set; }
        public string Sud { get; set; }
    }
}