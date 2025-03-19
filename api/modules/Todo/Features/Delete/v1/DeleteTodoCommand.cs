﻿using System.ComponentModel;
using MediatR;

namespace NexKoala.WebApi.Todo.Features.Delete.v1;
public sealed record DeleteTodoCommand(
    Guid Id) : IRequest;



