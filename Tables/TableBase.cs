using System;
using System.Collections.Generic;

namespace ChartCsApp.Tables
{
    abstract class TableBase
    {
        public abstract Dictionary<string, object> GetPrimaryKeys();
        public abstract string GetTableName();
        public abstract bool deleted { get; set; }
    }
}
