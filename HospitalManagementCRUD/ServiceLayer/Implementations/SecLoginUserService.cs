using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using HospitalManagementCRUD.CommonFunctions;
using AutoMapper;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class SecLoginUserService:ISecLoginUserService
    {
        ISecLoginUserRepo _secLoginUserRepo;
        MyMapper _myMapper;
        IMapper _mapper;
        public SecLoginUserService(ISecLoginUserRepo secLoginUserRepo , MyMapper myMapper , IMapper mapper)
        {
            _secLoginUserRepo = secLoginUserRepo;
            _myMapper = myMapper;
            _mapper = mapper;
        }

        public async Task<SecLoginUserDTO?> GetSecLoginUser(int userId)
        {
            SecLoginUser? secLoginUser = await _secLoginUserRepo.GetSecLoginUser(userId);

            if(secLoginUser==null)
                return null;

            //SecLoginUserDTO? secLoginUserDTO=new SecLoginUserDTO();
            //secLoginUserDTO= _myMapper.MapEntityToDto<SecLoginUser, SecLoginUserDTO>(secLoginUser, secLoginUserDTO);

            SecLoginUserDTO secLoginUserDTO= _mapper.Map<SecLoginUserDTO>(secLoginUser);

            return secLoginUserDTO;
        }

        public async Task<bool> SaveSecLoginUser(SecLoginUserDTO secLoginUserDTO)
        {
            bool isSave=false;
            if (secLoginUserDTO != null && secLoginUserDTO.UserName!=null)
            {
                if(await _secLoginUserRepo.CheckUsernameExists(secLoginUserDTO.UserName))
                {
                    isSave = false;
                }
                    
            }
            secLoginUserDTO.Password = Common.HashPassword(secLoginUserDTO.Password);
            SecLoginUser secLoginUser = _mapper.Map<SecLoginUser>(secLoginUserDTO);
            try
            {
                await _secLoginUserRepo.SaveSecLoginUser(secLoginUser);
                isSave=true;
            }
            catch (Exception ex)
            {
                isSave = false ;
            }
            return isSave;
        }
    }
}
