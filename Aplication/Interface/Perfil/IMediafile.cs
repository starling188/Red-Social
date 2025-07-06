

using Domain.Entities;
using Aplication.Dtos.perfilsmodel;


namespace Aplication.Interface.Perfil
{
    public interface IMediafile : IGenericService<MediaFileViewModel, MediaFile>
    {
        Task UploadMediaFileAsync(MediaFileViewModel viewModel);
        Task Save();
    }
}
