using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

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
    if (folhaConflito != null) {
        return Results.BadRequest("Folha já existe para o CPF, mês e ano informados\n");
    }

    ctx.FolhaPagamento.Add(folha);

    ctx.SaveChanges();

    return Results.Created("", folha);
});

app.MapGet("/api/folha/listar", () => "Lista");

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", () => "Busca");

app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", () => "Remoção");

app.MapGet("/api/folha/total-liquido", () => "Total");

app.Run();