using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.Runtime;

namespace NetMag.Console
{
    public class DTSX
    {
        public string ExecutarPacoteLocal(string caminhoPacote)
        {   
            Application app = new Application();
            Package pacote = app.LoadPackage(caminhoPacote, null);
            DTSExecResult resultado = pacote.Execute();

            return resultado.ToString();
        }

        public string ExecutarPacoteRemoto(string connString, string nomeJob)
        {
            string resultado;

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("sp_start_job", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter jobReturnValue = new SqlParameter(
                "@RETURN_VALUE", SqlDbType.Int);
            jobReturnValue.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(jobReturnValue);

            SqlParameter jobParameter = new SqlParameter(
                "@job_name", SqlDbType.VarChar);
            jobParameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(jobParameter);
            jobParameter.Value = nomeJob;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                int jobResultado = 
                    (Int32)command.Parameters["@RETURN_VALUE"].Value;

                switch (jobResultado)
                {
                    case 0:
                        resultado = "Pacote iniciado com sucesso.";
                        break;
                    default:
                        resultado = "Pacote falhou ao iniciar.";
                        break;
                }
            }
            catch (Exception exception)
            {
                resultado = exception.ToString();
            }
            finally
            {
                conn.Close();
            }

            return resultado;
        }
    }
} 