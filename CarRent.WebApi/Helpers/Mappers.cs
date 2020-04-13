using CarRent.Entities;
using CarRent.WebApi.Models;

namespace CarRent.WebApi.Helpers
{
    /// <summary>
    /// This class will impliment logic of mapping from model to viewModel and from viewModel to model
    /// </summary>
    public class Mappers
    {
        //Casting from car business logic to car view model:
        public static CarView MapCarEntityToCarView(CarEntity carModel)
        {
            return new CarView
            {
                Id = carModel.Id,
                Image = carModel.Image,
                Model = carModel.Model,
                PricePerDay = carModel.PricePerDay,
                CarNumber = carModel.CarNumber,
                Manufacturer = carModel.Manufacturer
            };
        }

        //Casting from car view model logic to car business logic:
        public static CarEntity MapCarViewToCarEntity(CarView carViewModel)
        {
            return new CarEntity
            {
                Image = carViewModel.Image,
                Model = carViewModel.Model,
                PricePerDay = carViewModel.PricePerDay,
                CarNumber = carViewModel.CarNumber,
                Manufacturer = carViewModel.Manufacturer
            };
        }

        //Casting from user business logic to user view model:
        public static UserView MapUserEntityToUserView(UserEntity userEntity)
        {
            return new UserView
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Password = userEntity.Password,
                PhoneNumber = userEntity.PhoneNumber,
                Email = userEntity.Email,
                DateOfBirth = userEntity.DateOfBirth,
                Role = userEntity.Role
            };
        }

        //Casting from user view model to user business logic:
        public static UserEntity MapUserViewToUserEntity(UserView userView)
        {
            return new UserEntity
            {
                Id = userView.Id,
                FirstName = userView.FirstName,
                LastName = userView.LastName,
                Password = userView.Password,
                PhoneNumber = userView.PhoneNumber,
                Email = userView.Email,
                DateOfBirth = userView.DateOfBirth,
                Role = userView.Role
            };
        }

        //Casting from credentials view model to credentials businnes logic:
        public static CredentialsEntity MapCredentialsViewToCredentialsEntity(CredentialsView credentialsView)
        {
            return new CredentialsEntity
            {
                Email = credentialsView.Email,
                Password = credentialsView.Password
            };
        }

        //Casting from credentials businnes logic to credentials view model:
        public static CredentialsView MapCredentialsEntityToCredentialsView(CredentialsEntity credentialsEntity)
        {
            return new CredentialsView
            {
                Email = credentialsEntity.Email,
                Password = credentialsEntity.Password
            };
        }

        //Casting from order mode bussines logic to view order model:
        public static OrderView MapOrderEntityToOrderView(OrderEntity orderModel)
        {
            return new OrderView
            {
                Id = orderModel.Id,
                CarId = orderModel.CarId,
                FromDate = orderModel.FromDate,
                ToDate = orderModel.ToDate,
                TotalCost = orderModel.TotalCost
            };
        }

        //Casting from order view model to order mode bussines logic:
        public static OrderEntity MapOrderViewToOrderEntity(OrderView orderView)
        {
            return new OrderEntity
            {
                Id = orderView.Id,
                CarId = orderView.CarId,
                FromDate = orderView.FromDate,
                ToDate = orderView.ToDate,
                TotalCost = orderView.TotalCost,
                UserId = (int)orderView.UserId
            };
        }

        //// Casting user bussines logic model to user view model:
        //public static AuthenticationViewModel MapUserModelToAuthenticationViewModel(UserModel userModel)
        //{
        //    AuthenticationViewModel authenticationViewModel = new AuthenticationViewModel();

        //    authenticationViewModel.firstName = userModel.firstName;
        //    authenticationViewModel.lastName = userModel.lastName;

        //    return authenticationViewModel;
        //}

        ////Casting autentication view model to user bussines logic model:
        //public static UserModel MapAuthenticationViewModelToUserModel(AuthenticationViewModel authenticationViewModel)
        //{
        //    UserModel userModel = new UserModel();

        //    userModel.firstName = authenticationViewModel.firstName;
        //    authenticationViewModel.lastName = userModel.lastName;

        //    return userModel;
        //}

        ////Casting user order bussines logic model to user order view model:
        //public static UserOrderModel MapUserViewOrderModelToUserOrderModel(UserViewOrderModel userViewOrderModel)
        //{
        //    UserOrderModel userOrderModel = new UserOrderModel();

        //    userOrderModel.photo = userViewOrderModel.photo;
        //    userOrderModel.manufacturer = userViewOrderModel.manufacturer;
        //    userOrderModel.model = userViewOrderModel.model;
        //    userOrderModel.fromDate = userViewOrderModel.fromDate;
        //    userOrderModel.toDate = userViewOrderModel.toDate;
        //    userOrderModel.totalCost = userViewOrderModel.totalCost;

        //    return userOrderModel;
        //}

        ////Casting from user orders bussines logic to users orders view model:
        //public static UserViewOrderModel MapUserOrderModelToUserViewOrderModel(UserOrderModel userOrderModel)
        //{
        //    UserViewOrderModel userViewOrderModel = new UserViewOrderModel();

        //    userViewOrderModel.photo = userOrderModel.photo;
        //    userViewOrderModel.firstName = userOrderModel.firstName;
        //    userViewOrderModel.lastName = userOrderModel.lastName;
        //    userViewOrderModel.manufacturer = userOrderModel.manufacturer;

        //    userViewOrderModel.model = userOrderModel.model;
        //    userViewOrderModel.fromDate = userOrderModel.fromDate;
        //    userViewOrderModel.toDate = userOrderModel.toDate;
        //    userViewOrderModel.totalCost = userOrderModel.totalCost;

        //    return userViewOrderModel;
        //}
        //Casting from manufacturer view model to manufacturer mode bussines logic:
        public static ManufacturerEntity MapManufacturerViewToManufacturerEntity(ManufacturerView manufacturerViewModel)
        {
            return new ManufacturerEntity
            {
                Id = manufacturerViewModel.Id,
                Manufacturer = manufacturerViewModel.Manufacturer
            };

        }

        //Casting from manufacturer bussines logic to manufacturer view model :
        public static ManufacturerView MapManufacturerEntityToManufacturerView(ManufacturerEntity manufacturer)
        {
            return new ManufacturerView
            {
                Id = manufacturer.Id,
                Manufacturer = manufacturer.Manufacturer
            };
        }

        //Casting from car view model to car bussines logic model:
        public static ModelCarEntity MapModelCarViewToCarEntity(ModelCarView modelCarView)
        {
            return new ModelCarEntity
            {
                Id = modelCarView.Id,
                Model = modelCarView.Model,
                ManufacturerId = modelCarView.ManufacturerId,
                PricePerDay = modelCarView.PricePerDay,
                Image = modelCarView.Photo
            };
        }

        //Casting from car bussines logic model to car view model:
        public static ModelCarView MapCarEntityToModelCarView(ModelCarEntity modelCarEntity)
        {
            return new ModelCarView
            {
                Id = modelCarEntity.Id,
                Model = modelCarEntity.Model,
                ManufacturerId = modelCarEntity.ManufacturerId,
                PricePerDay = modelCarEntity.PricePerDay,
                Photo = modelCarEntity.Image
            };
        }

        ////Casting from company fleet view model to company fleet bussines logic model:
        //public static CompanyFleetModel MapCompanyFleetViewModelToCompanyFleetModel(CompanyFleetViewModel companyFleetViewModel)
        //{
        //    CompanyFleetModel companyFleetModel = new CompanyFleetModel();

        //    companyFleetModel.id = companyFleetViewModel.id;
        //    companyFleetModel.modelId = companyFleetViewModel.modelId;
        //    companyFleetModel.carNumber = companyFleetViewModel.carNumber;

        //    return companyFleetModel;
        //}

        ////Casting from company fleet bussines logic model to company fleet view model:
        //public static CompanyFleetViewModel MapCompanyFleetModelToCompanyFleetVIewModel(CompanyFleetModel companyFleetModel)
        //{
        //    CompanyFleetViewModel companyFleetViewModel = new CompanyFleetViewModel();

        //    companyFleetViewModel.id = companyFleetModel.id;
        //    companyFleetViewModel.modelId = companyFleetModel.modelId;
        //    companyFleetViewModel.carNumber = companyFleetModel.carNumber;

        //    return companyFleetViewModel;
        //}
    }
}
