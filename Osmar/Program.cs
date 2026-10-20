using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapPost("/api/folha/cadastrar", ([FromBody] FolhaPagamento? folha, [FromServices] AppDataContext ctx) => {
    if (folha == null) {
        return Results.BadRequest("Informações inválidas\n");
    }

    if (folha.Mes < 1 || folha.Mes > 12) {
        return Results.BadRequest("Mês inválido\n");
    }

    if (folha.Ano < 2000) {
        return Results.BadRequest("Ano inváido\n");
    }

    var folhaConflito = ctx.FolhaPagamento.Any(
        f => f.Cpf == folha.Cpf && f.Mes == folha.Mes && f.Ano == folha.Ano
    );
    if (folhaConflito) {
        return Results.BadRequest("Folha já existe para o CPF, mês e ano informados\n");
    }

    ctx.FolhaPagamento.Add(folha);

    ctx.SaveChanges();

    return Results.Created("", folha);
});

app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) => {
    if (ctx.FolhaPagamento.FirstOrDefault() == null) {
        return Results.NotFound("Nenhuma folha encontrada\n");
    }

    return Results.Ok(ctx.FolhaPagamento);
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", ([FromServices] AppDataContext ctx, [FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano) => {
    var folha = ctx.FolhaPagamento.FirstOrDefault(f => f.Cpf == cpf && f.Mes == mes && f.Ano == ano);
    if (folha == null) {
        return Results.NotFound("Folha não encontrada\n");
    }

    return Results.Ok(folha);
});

app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", ([FromServices] AppDataContext ctx, [FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano) => {
    var folha = ctx.FolhaPagamento.FirstOrDefault(f => f.Cpf == cpf && f.Mes == mes && f.Ano == ano);
    if (folha == null) {
        return Results.NotFound("Folha não encontrada\n");
    }

    ctx.FolhaPagamento.Remove(folha);

    ctx.SaveChanges();

    return Results.NoContent();
});

app.MapGet("/api/folha/total-liquido", ([FromServices] AppDataContext ctx) => {
    if (ctx.FolhaPagamento.FirstOrDefault() == null) {
        return Results.NotFound("Nenhuma folha encontrada\n");
    }
    
    double total = 0;
    foreach (var f in ctx.FolhaPagamento) {
        total += f.SalarioLiquido;
    }

    return Results.Ok($"{total}");
});

app.Run();