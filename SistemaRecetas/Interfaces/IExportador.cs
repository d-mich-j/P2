using SistemaRecetas.Modelos;

namespace SistemaRecetas.Interfaces;

public interface IExportador
{
    void ExportarATxt(Usuario usuario, string rutaArchivo);
}
