using DigitalNotes.API.Extensions;
using DigitalNotes.Application.Notes.Commands.CreateNote;
using DigitalNotes.Application.Notes.Commands.UpdateNote;
using DigitalNotes.Application.Notes.Queries.GetLastRowNumber;
using DigitalNotes.Application.Notes.Queries.GetNote;
using DigitalNotes.Application.Notes.Queries.GetNotes;
using MediatR;

namespace DigitalNotes.API.Endpoints;

public static class NoteEndpoints
{
    public static void MapNoteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/note").WithTags("Note").WithOpenApi();

        group.MapPost("/",
                async (ISender sender, CreateNoteCommand command, CancellationToken ct) =>
                    await sender.Send(command, ct))
            .WithName("CreateNote")
            .ProducesPost();

        group.MapGet("/",
                async (ISender sender, [AsParameters] GetNoteQuery query, CancellationToken ct) =>
                await sender.Send(query, ct))
            .WithName("GetNote")
            .ProducesGet<NoteDto>();

        group.MapPut("/{id:guid}",
                async (ISender sender, Guid id, UpdateNoteCommand command, CancellationToken ct) =>
                {
                    if (id != command.Id) return Results.BadRequest();
                    await sender.Send(command, ct);
                    return Results.NoContent();
                })
            .WithName("UpdateNote")
            .ProducesPut();

        // TODO: move?
        group.MapGet("/{createdBy}/{lastRowNumber:int}",
                async (ISender sender, string createdBy, int lastRowNumber, CancellationToken ct) =>
                    await sender.Send(new GetNotesQuery {LastRowNumber = lastRowNumber, CreatedBy = createdBy}, ct))
            .WithName("GetNotes")
            .ProducesGet<IReadOnlyCollection<NoteDto>>();

        group.MapGet("/{createdBy}",
                async (ISender sender, string createdBy, CancellationToken ct) =>
                    await sender.Send(new GetLastRowNumberQuery {CreatedBy = createdBy}, ct))
            .WithName("GetLastRowNumber")
            .ProducesGet<int?>();
    }
}