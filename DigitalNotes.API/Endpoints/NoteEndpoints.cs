using DigitalNotes.API.Extensions;
using DigitalNotes.Application.Notes.Commands.CreateNote;
using DigitalNotes.Application.Notes.Commands.UpdateNote;
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
        group.MapGet("/{createdBy}",
                async (ISender sender, string createdBy, CancellationToken ct, string? noteNameQuery,
                        int pageNumber = 1, int pageSize = 5
                    ) =>
                    await sender.Send(
                        new GetNotesQuery
                        {
                            PageNumber = pageNumber,
                            PageSize = pageSize,
                            CreatedBy = createdBy,
                            NoteNameQuery = noteNameQuery
                        }, ct))
            .WithName("GetNotes")
            .ProducesGet<NotesDto>();
    }
}