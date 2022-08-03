using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Models
{
  public class ReturnModel<T>
  {
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public string[] Errors { get; set; }
  }
  public class ReturnModel
  {
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public string[] Errors { get; set; }
  }
}
