using Dal;
using System;

namespace Services
{
    public class ServiceBase
    {
        protected IUnitOfWork _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
