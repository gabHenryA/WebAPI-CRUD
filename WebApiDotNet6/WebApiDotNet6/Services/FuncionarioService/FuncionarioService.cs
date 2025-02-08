using Microsoft.EntityFrameworkCore;
using WebApiDotNet6.DataContext;
using WebApiDotNet6.Models;

namespace WebApiDotNet6.Services.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly AplicationDBContext _context;
        public FuncionarioService(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                if(novoFuncionario == null)
                {
                    serviceResponse.Mensagem = "Por favor informe os dados";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Add(novoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Remove(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Mensagem = "Funcionário excluído";
                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel>();
            
            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);
                
                if(funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionario não encontrado";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = funcionario;
                serviceResponse.Mensagem = "Funcionario encontrado";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();

                if(serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if(funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                funcionario.Ativo = false;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();
                
                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionario inativado";
            }
            catch (Exception ex)
            {
                 serviceResponse.Mensagem = ex.Message;
                 serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == editadoFuncionario.Id);

                if(funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                editadoFuncionario.DataDeAlteracao = DateTime.Now.ToLocalTime();


                _context.Update(editadoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário editado";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
