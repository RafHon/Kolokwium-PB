﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IHistoriaService
    {
        Task<IEnumerable<Historia>> GetPagedFromProcedureAsync(int pageNumber, int pageSize);
    }
}
