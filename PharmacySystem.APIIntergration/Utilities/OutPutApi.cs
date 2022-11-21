using Newtonsoft.Json;
using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration.Utilities
{
    public static class OutPutApi
    {
        public static List<T> OutPut<T>(RequestResponse body)
        {
            if (body.Status == 0 || body != null)
            {
                try
                {
                    List<T> data = (List<T>)JsonConvert.DeserializeObject(body.Content, typeof(List<T>));

                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        public static string OutPutString<T>(RequestResponse body)
        {
            if (body.Status == 0 || body != null)
            {
                return body.Content;
            }
            return null;
        }


        public static bool OutPutBool<T>(RequestResponse body)
        {
            if (body.Status == 0 && body != null)
            {
                return true;
            }
            return false;
        }

        public static bool OutPutAddFile<T>(RequestResponse body)
        {
            if (body.Status == 0 || body != null)
            {
                try
                {
                    List<T> data = (List<T>)JsonConvert.DeserializeObject(body.Content, typeof(List<T>));

                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return false;
        }
    }
}
