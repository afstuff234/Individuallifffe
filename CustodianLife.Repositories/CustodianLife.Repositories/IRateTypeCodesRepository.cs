﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianLife.Model;

namespace CustodianLife.Repositories
{
    public interface IRateTypeCodesRepository:IRepository<RateTypeCodes,Int32?>
    {
        IList<RateTypeCodes> RateTypeCodesDetails();
        RateTypeCodes GetById(String _key);
    }
}
