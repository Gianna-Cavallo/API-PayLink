using Microsoft.AspNetCore.Mvc;
using PayLink.Models;
using PayLink.Services;

namespace PayLink.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // La ruta será: api/payments
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        // El controlador recibe el servicio por inyección de dependencias.
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetAll()
        {
            return Ok(_paymentService.GetAll());
        }

        // GET: api/payments/{id}
        [HttpGet("{id}")]
        public ActionResult<Payment> GetById(int id)
        {
            var payment = _paymentService.GetById(id);
            if (payment == null)
                return NotFound($"No se encontró el pago con ID {id}");
            return Ok(payment);
        }

        // POST: api/payments
        [HttpPost]
        public ActionResult<Payment> Create(Payment payment)
        {
            try
            {
                var newPayment = _paymentService.Create(payment);
                // Devuelve 201 (Created) con la ubicación del recurso nuevo.
                return CreatedAtAction(nameof(GetById), new { id = newPayment.Id }, newPayment);
            }
            catch (Exception ex)
            {
                // Si el negocio no existe o los datos están mal, devuelve 400 (Bad Request).
                return BadRequest(ex.Message);
            }
        }
    }
}
