﻿using InventarioBackend.src.Core.Application.Clients.DTOs;
using InventarioBackend.src.Core.Domain.Clients.Entities;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class ClientMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Mapear entidad Client a ClientDto
            config.NewConfig<Client, ClientDto>()
                .Map(dest => dest.EntitiName, src => src.EntitiConfigs.EntitiName);

            // Mapear DTO de creación a entidad Client
            config.NewConfig<ClientCreateDto, Client>();

            // Opcional: DTO para actualización (si tienes ClientUpdateDto)
            // config.NewConfig<ClientUpdateDto, Client>();
        }
    }
}
