using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachine.Service.StateMaps;

namespace SagaStateMachine.Service.StateDbContexts;

// sagadbcontext veritabanı oluşturma
public class OrderStateDbContext(DbContextOptions options) : SagaDbContext(options)
{
    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {//validasyon kuralları 
            yield return new OrderStateMap();
        }
    }
}
