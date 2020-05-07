﻿using System.Collections.Generic;

namespace Quartz.AspNetCore.Server.Dtos
{
    public class JobHistoryViewModel
    {
        public JobHistoryViewModel(IReadOnlyList<JobHistoryEntryDto> entries, string errorMessage)
        {
            HistoryEntries = entries;
            ErrorMessage = errorMessage;
        }

        public IReadOnlyList<JobHistoryEntryDto> HistoryEntries { get; }
        public string ErrorMessage { get; }
    }
}