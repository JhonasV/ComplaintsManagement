using ComplaintsManagement.Infrastructure.Database;
using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.UI.Services.Repositories
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomersRepository _customersRepository;
        private readonly IServiceRateRepository _serviceRateRepository;
        private readonly IStatusRepository _statusRepository;

        public ComplaintsRepository(ApplicationDbContext context, ICustomersRepository customersRepository, IServiceRateRepository serviceRateRepository, IStatusRepository statusRepository)
        {
            _context = context;
            _customersRepository = customersRepository;
            _serviceRateRepository = serviceRateRepository;
            _statusRepository = statusRepository;
        }
        public async Task<TaskResult<ComplaintsDto>> DeleteAsync(int Id)
        {
            var complaints = await _context.Complaints.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<ComplaintsDto>();
            try
            {
                if (complaints != null)
                {
                    complaints.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Queja borrado exitosamente!";
                    result.Data = new ComplaintsDto { Active = complaints.Active, CreatedAt = complaints.CreatedAt,  Id = complaints.Id,  StatusId = complaints.StatusId, ProductsId = complaints.ProductsId, ComplaintsOptionsId = complaints.ComplaintsOptionsId };
                }
                else
                {
                    result.Message = "No se pudo encontrar la queja con el Id enviado";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task ExportAll(HttpResponseBase Response)
        {
            var complaints = await this.GetAllAsync();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Tickets");

            ws.Cells["A1"].Value = "Cliente";
            ws.Cells["B1"].Value = "Tipo";
            ws.Cells["C1"].Value = "Producto";
            ws.Cells["D1"].Value = "Departamento";
            ws.Cells["E1"].Value = "Fecha de Creacion";
            ws.Cells["F1"].Value = "Fecha de Cierre";
            ws.Cells["G1"].Value = "Estado";
            ws.Cells["H1"].Value = "Razon";
            ws.Cells["I1"].Value = "Comentario";


            int rowStart = 2;
            foreach (var item in complaints.Data)
            {
                ws.Cells[$"A{rowStart}"].Value = $"{item.Customer.Name} {item.Customer.LastName}";
                ws.Cells[$"B{rowStart}"].Value = item.TicketType.Description;
                ws.Cells[$"C{rowStart}"].Value = item.Product.Name;
                ws.Cells[$"D{rowStart}"].Value = item.Deparment.Name;
                ws.Cells[$"E{rowStart}"].Value = item.CreatedAt.ToString("MM/dd/yyyy hh:mm tt");
                ws.Cells[$"F{rowStart}"].Value = item.CompletedAt?.ToString("MM/dd/yyyy hh:mm tt");
                ws.Cells[$"G{rowStart}"].Value = item.Status.Name;
                ws.Cells[$"H{rowStart}"].Value = item.ComplaintsOption.Name;
                ws.Cells[$"I{rowStart}"].Value = item.Comment;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + $"Tickets-{DateTime.Now:MM-dd-yyyy-hh:mm-ss}" + ".xlsx");
                pck.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public async Task ExportPorcentage(HttpResponseBase Response)
        {

            var porcentages = new List<ReportPorcentageModel>();
            var complaints = await this.GetAllAsync();
            var totalPerStatus = 0;
            var totalTickets = complaints.Data.Count();
            var complaintsGrouped = complaints.Data.GroupBy(e => e.StatusId).ToList();
            foreach (var item in complaintsGrouped)
            {
                var porcentage = new ReportPorcentageModel();
                totalPerStatus = item.ToList().Count;
                decimal result = (decimal)totalPerStatus / (decimal)totalTickets;
                porcentage.StatusPorcentage = result * 100;
                porcentage.Status = item.FirstOrDefault().Status.Name;

                porcentages.Add(porcentage);
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Tickets");

            ws.Cells["A1"].Value = "Estado";
            ws.Cells["B1"].Value = "Porcentaje";
           



            int rowStart = 2;
            foreach (var item in porcentages)
            {
                ws.Cells[$"A{rowStart}"].Value = item.Status;
                ws.Cells[$"B{rowStart}"].Value = $"{item.StatusPorcentage:0.##}%";
  

                rowStart++;
            }

            ws.Cells[$"A{complaints.Data.Count() + 1}"].Value = "Total";
            ws.Cells[$"B{complaints.Data.Count() + 1}"].Value = complaints.Data.Count();
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + $"Tickets-status-{DateTime.Now:MM-dd-yyyy-hh:mm-ss}" + ".xlsx");
                pck.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

        }

        public async Task ExportServiceRate(HttpResponseBase Response)
        {
            var rates = await _serviceRateRepository.GetAllAsync();
            var rateGrouped = rates.Data.GroupBy(e => e.Rate).ToList();
            var totalRates = rates.Data.Count();
            var porcentages = new List<ServiceRatePortenageModel>();
            var totalPerRate = 0;
            foreach (var item in rateGrouped)
            {

                totalPerRate = item.ToList().Count;
                decimal result = (decimal)totalPerRate / (decimal)totalRates;
                var porcentage = new ServiceRatePortenageModel();

                porcentage.Rate = Enum.GetName(typeof(EnumRate), item.FirstOrDefault().Rate);
                porcentage.RatePorcentage = result * 100;


                porcentages.Add(porcentage);
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Tickets");

            ws.Cells["A1"].Value = "Puntuacion";
            ws.Cells["B1"].Value = "Porcentaje";




            int rowStart = 2;
            foreach (var item in porcentages)
            {
                ws.Cells[$"A{rowStart}"].Value = item.Rate;
                ws.Cells[$"B{rowStart}"].Value = $"{item.RatePorcentage.ToString("0.##")}%";


                rowStart++;
            }

            ws.Cells[$"A{rates.Data.Count() + 1}"].Value = "Total";
            ws.Cells[$"B{rates.Data.Count() + 1}"].Value = rates.Data.Count();
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + $"Tickets-rate-{DateTime.Now.ToString("MM-dd-yyyy-hh:mm-ss")}" + ".xlsx");
                pck.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public async Task<TaskResult<List<ComplaintsDto>>> GetAllAsync()
        {
           
            List<ComplaintsDto> complaintsDtos = new List<ComplaintsDto>();
            var result = new TaskResult<List<ComplaintsDto>>();
            try
            {
                var complaints = await _context
                    .Complaints
                    .Include(e => e.ComplaintsOption)
                    .Include(e => e.Status)
                    .Include(e => e.Product)
                    .Include(e => e.Deparment)
                    .Include(e => e.TicketType)
                    .Where(e => e.Active)
                    .ToListAsync();



      
                var complaintsDto = AutoMapper.Mapper.Map<List<ComplaintsDto>>(complaints);
                foreach (var complaint in complaintsDto)
                {
                    var customer = await _customersRepository.GetAsync(complaint.UsersId);
                    complaint.Customer = customer.Data;
                }


                result.Data = complaintsDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de quejas: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<List<ComplaintsDto>>> GetAllByUserIdAsync(string userId)
        {
            var allComplaints = await this.GetAllAsync();
            TaskResult<List<ComplaintsDto>> result = new TaskResult<List<ComplaintsDto>>();
            result.Data = allComplaints.Data.Where(e => e.UsersId == userId).ToList();
            return result;
        }

        public async Task<TaskResult<ComplaintsDto>> GetAsync(int Id)
        {
            var result = new TaskResult<ComplaintsDto>();

            try
            {
                var complaints = 
                    await _context
                    .Complaints
                    .Include(e => e.ComplaintsOption)
                    .Include(e => e.Status)
                    .Include(e => e.Product)
                    .Include(e => e.Deparment)
                    .Include(e => e.TicketType)
                    .Include(e => e.Binnacles.Select(b => b.Status))
                    .FirstOrDefaultAsync(e => e.Id == Id && e.Active);

                var customer = await _customersRepository.GetAsync(complaints.UsersId);

                var complaintsDto = AutoMapper.Mapper.Map<ComplaintsDto>(complaints);
                complaintsDto.Customer = customer.Data;

                foreach (var binnacle in complaintsDto.Binnacles)
                {
                    var user = await _customersRepository.GetAsync(binnacle.ApplicationUserId);
                    binnacle.User = AutoMapper.Mapper.Map<UsersDto>(user.Data);
                }

                result.Data = complaintsDto;

            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;
             
        }

        public async Task<TaskResult<ComplaintsDto>> SaveAsync(ComplaintsDto complaintsDto)
        {

            var complaints = AutoMapper.Mapper.Map<Complaints>(complaintsDto);


            var result = new TaskResult<ComplaintsDto>();
            try
            {
                _context.Complaints.Add(complaints);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó la queja #{complaints.Id}";
                result.Data = AutoMapper.Mapper.Map<ComplaintsDto>(complaints);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la queja: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<ComplaintsDto>> UpdateAsync(ComplaintsDto complaintsDto)
        {

            var complaints = AutoMapper.Mapper.Map<Complaints>(complaintsDto);

            var result = new TaskResult<ComplaintsDto>();
            try
            {
                _context.Complaints.Add(complaints);
                _context.Entry(complaints).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = complaintsDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información de la queja: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<ComplaintsDto>> UpdateStatusAsync(int statusId, int complaintsId)
        {

            var result = new TaskResult<ComplaintsDto>();
            try
            {
                var complaints = await _context.Complaints.Include(e => e.Status).Where(e => e.Id == complaintsId && e.Active).FirstOrDefaultAsync();
                var status = await _statusRepository.GetAsync(statusId);
                complaints.StatusId = statusId;


                if (status.Data.Name.ToUpper() == StatusName.CLOSED)
                {
                    complaints.CompletedAt = DateTime.Now;
                    complaints.UpdatedAt = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                result.Data = AutoMapper.Mapper.Map<ComplaintsDto>(complaints);
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información de la queja: {e.Message}";
            }
            return result;
            //var complaintDto = AutoMapper.Mapper.Map<ComplaintsDto>(complaint);
            //var complaintUpdated = await this.UpdateAsync(complaintDto);

        }
    }
}