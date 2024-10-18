interface IServiceHandler<TRequest, TResponse>
    where TRequest : GSFService.GSFRequest
    where TResponse : GSFService.GSFResponse
{
    TResponse Handle(TRequest request);
}
