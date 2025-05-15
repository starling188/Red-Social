

using Domain.Entities;
using Domain.Models.perfilsmodel;


namespace Domain.Interface.Service.Perfil
{
    public interface IMediafile : IGenericService<MediaFileViewModel, MediaFile>
    {
        Task UploadMediaFileAsync(MediaFileViewModel viewModel);
        Task Save();
    }
}
