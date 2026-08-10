using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Create;

public static class StatusCreater
{
    public static Status Create(
       StatusCreateRequest request)
    {
        return new Status
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description ?? ""
        };
    }
}