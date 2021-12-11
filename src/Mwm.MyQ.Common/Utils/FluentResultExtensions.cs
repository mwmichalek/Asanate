using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Common.Utils {
    public static class FluentResultExtensions {

        public static bool TryUsing<TResult>(this Result<TResult> result, out TResult value) where TResult : class {
            if (result.IsSuccess) {
                value = result.Value;
                return true;
            } else {
                value = default;
                return false;
            }

        }

    }
}
