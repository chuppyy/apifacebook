using ITC.Domain.ResponseDto;
using MediatR;
using NCore.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.Domain.Commands.GoogleAnalytics.Models
{
    public class GetDataFromStringQuery : IRequest<JsonResponse<ResultXYDto>>
    {
        public string Key { get; set; }
    }
}
