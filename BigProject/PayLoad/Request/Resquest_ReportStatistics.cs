﻿namespace BigProject.PayLoad.Request
{
    public class Resquest_ReportStatistics
    {
        public string ReportName { get; set; }
        public string Description { get; set; }
        public int ReportTypeId { get; set; }
        public int UserId { get; set; }
    }
}
