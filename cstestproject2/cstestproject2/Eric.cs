

var query = context.Appointments
.Join(
context.Services,
appointment => appointment.ServId,
service => service.ServId,
(appointment, service) => new
{
    ServId = service.ServId,
    Start = appointment.StartDateTime,
    End = appointment.EndDateTime,
}
).ToList();
