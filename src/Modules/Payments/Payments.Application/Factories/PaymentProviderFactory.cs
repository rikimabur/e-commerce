using Payments.Application.Abstractions;

namespace Payments.Application.Factories;
public sealed class PaymentProviderFactory
{
    private readonly IEnumerable<IPaymentProvider> _providers;

    public PaymentProviderFactory(IEnumerable<IPaymentProvider> providers)
    {
        _providers = providers;
    }

    public IPaymentProvider Resolve(string provider) => _providers.Single(p =>
                                                        p.Name.Equals(provider, StringComparison.OrdinalIgnoreCase));
}