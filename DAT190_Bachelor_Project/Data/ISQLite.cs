﻿using System;
using SQLite;

namespace DAT190_Bachelor_Project.Data
{
    public interface ISQLite
    {
        string GetLocalFilePath(string fileName);
    }
}
