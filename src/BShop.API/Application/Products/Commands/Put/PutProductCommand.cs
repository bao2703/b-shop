﻿using System.Text.Json.Serialization;
using MediatR;

namespace BShop.API.Application.Products.Commands.Put
{
    public class PutProductCommand : IRequest
    {
        [JsonIgnore]
        public long Id { get; set; }

        public string? Name { get; set; }
    }
}
