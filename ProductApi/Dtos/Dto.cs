namespace ProductApi.Dtos
{
    public class Dto
    {
        public record ProductDto(Guid Id, string Name, int Price, DateTime Createdtime);
        public record CreateProductDto(string Name, int Price);
    }
}
