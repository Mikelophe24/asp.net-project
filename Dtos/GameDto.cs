namespace GameStore.api.Dtos;

 // DTO như một cái hợp đồng quyết định cashe data sẽ được truyền đi như thế nào và sử dụng ra sao giữa sclint và server


public record  GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price, 
    DateOnly ReleaseDate00
);
