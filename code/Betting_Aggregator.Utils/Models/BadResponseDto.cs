namespace Betting_Aggregator.Utils
{
    public sealed class InternalServerErrorResponseDto : ResponseDto
    {
        public string CorrelationId { get; set; }
    }
}