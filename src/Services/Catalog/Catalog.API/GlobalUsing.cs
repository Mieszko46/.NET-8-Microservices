﻿global using Carter;
global using Mapster;
global using MediatR;
global using Marten;
global using FluentValidation;
global using BuildingBlocks.CQRS;
global using Catalog.API.Models;
global using Catalog.API.Exceptions;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.Exceptions.Handler;
global using Catalog.API.Data;

// It is a good practice to create global using C# class unser the catalog API
// If these usings are used in almost every file