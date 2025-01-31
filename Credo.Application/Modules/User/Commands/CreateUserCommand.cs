﻿using MediatR;

namespace Credo.Application.Modules.User.Commands;

public class CreateUserCommand : IRequest<int>
{
    public required Domain.Entities.User User { get; set; }
}
