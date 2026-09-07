using System.ComponentModel.DataAnnotations;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//In-memory list of books
var book_list = new List<Book>
{    
    new Book { Id = 1, Title = "The Pragmatic Programmer", Pages = 390 },
    new Book { Id = 2, Title = "Clean Code", Pages = 630 },
    new Book { Id = 3, Title = "Design Patterns", Pages = 505 }
};

// Create GET endpoint here
app.MapGet("/api/books", () => book_list)
   .WithName("BooksList")
   .WithSummary("Retrieve all books")
   .WithDescription("Returns a list of all books in the system")
   .WithTags("Books");
//POST endpoint to add new books
app.MapPost("/api/books", (Book book) =>
{
    Console.WriteLine($"Received: Id={book.Id}, Pages={book.Pages}");
    var ctx = new ValidationContext(book);
    var results = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(book, ctx, results, true);
    Console.WriteLine($"isValid: {isValid}, errors: {results.Count}");
    if(!isValid){
      return Results.BadRequest(results);
    }

    book_list.Add(book);
    return Results.Created($"/api/books/{book.Id}", book);
    
})
  .Accepts<Book>("application/json")
  .Produces<Book>(StatusCodes.Status201Created)
  .Produces<List<ValidationResult>>(StatusCodes.Status400BadRequest)
  .WithTags("Books");

app.MapPut("/api/books/{id}", (int id, Book new_book) => {
    var ctx = new ValidationContext(new_book);
    var results = new List<ValidationResult>();
    bool isValid = Validator.TryValidateObject(new_book, ctx, results, true);
    if (!isValid) {
        return Results.BadRequest(results);
    }

    for (int i = 0; i < book_list.Count; i++) {
        if (book_list[i].Id == id) {
            book_list[i] = new_book;
            return Results.Ok(new_book);
        }
    }
    return Results.NotFound();
})
  .WithName("UpdateBook")
  .WithSummary("Update an existing book")
  .Accepts<Book>("application/json")
  .Produces<Book>(StatusCodes.Status200OK)
  .Produces<List<ValidationResult>>(StatusCodes.Status400BadRequest)
  .Produces(StatusCodes.Status404NotFound)
  .WithTags("Books");

app.MapDelete("/api/books/{id}", (int id) => {
    var book_to_remove = book_list.FirstOrDefault(b => b.Id == id);
    if (book_to_remove is null) {
        return Results.NotFound();
    }
    book_list.Remove(book_to_remove);
    return Results.NoContent();
})
  .WithName("DeleteBook")
  .WithSummary("Delete a book by ID")
  .Produces(StatusCodes.Status204NoContent)
  .Produces(StatusCodes.Status404NotFound)
  .WithTags("Books");

app.Run();

public class Book
{
    [Required]
    [Range(1, 100000)]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1, 1000)]
    public int Pages { get; set; }
}
