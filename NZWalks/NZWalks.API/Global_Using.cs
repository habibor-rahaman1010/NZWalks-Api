﻿global using Microsoft.EntityFrameworkCore;
global using NZWalks.API.ExtensionMethods;
global using Serilog;
global using Serilog.Events;
global using Serilog.Sinks.MSSqlServer;
global using System.Reflection;
global using AutoMapper;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using NZWalks.API.Controllers.BaseController;
global using NZWalks.API.CustomActionFilters;
global using NZWalks.API.DomainEntities;
global using NZWalks.API.Dtos.DifficultiesDto;
global using NZWalks.API.ServicesInterface;
global using NZWalks.API.Utilities;
global using NZWalks.API.Dtos.RegionsDto;
global using NZWalks.API.Dtos.WalksDto;
global using Microsoft.AspNetCore.Mvc.Filters;
global using NZWalks.API.Data;
global using NZWalks.API.RepositoriesInterface;
global using NZWalks.API.UnitOfWorkInterface;
global using System.Linq.Expressions;
global using Microsoft.EntityFrameworkCore.Query;
global using Microsoft.AspNetCore.Identity;
global using NZWalks.API.NZWalksIdentity;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using NZWalks.API.Repositories;
global using NZWalks.API.Services;
global using NZWalks.API.UnitOfWorks;
global using NZWalks.API.IdentitySeeder;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;