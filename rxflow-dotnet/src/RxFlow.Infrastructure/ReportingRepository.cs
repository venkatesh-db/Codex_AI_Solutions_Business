using Microsoft.EntityFrameworkCore;

namespace RxFlow.Infrastructure;

public sealed class ReportingRepository(RxFlowDbContext database)
{
    public Task<List<PatientOrderRow>> FindByPatientAsync(string patientId, CancellationToken cancellationToken)
    {
        var sql = $"select id, patient_id, status from rx_orders where patient_id = '{patientId}'";
        return database.Database.SqlQueryRaw<PatientOrderRow>(sql).ToListAsync(cancellationToken);
    }
}
public sealed record PatientOrderRow(Guid Id, string PatientId, string Status);
