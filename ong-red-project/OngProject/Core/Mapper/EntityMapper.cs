﻿using System;
using System.Collections;
using System.Collections.Generic;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.DTOs.UserDTOs;
using OngProject.Core.Entities;
using OngProject.Infrastructure.Repositories;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        #region News Mappers

        public NewsDTO FromNewsToNewsDTO(News news)
        {
            var newsDTO = new NewsDTO()
            {
                Name = news.Name,
                Content = news.Content,
                Image = news.Image,
                CategoryId = news.CategoryId
            };
            return newsDTO;
        }

        public News FromNewsDTOtoNews(NewsDTO newsDTO)
        {
            var news = new News()
            {
                Name = newsDTO.Name,
                Content = newsDTO.Content,
                Image = newsDTO.Image,
                CategoryId = newsDTO.CategoryId
            };
            return news;
        }

        #endregion News Mappers

        #region Member Mapper

        public MembersDTO FromMembersToMembersDto(Member member)
        {
            var membersDTO = new MembersDTO()
            {
                Name = member.Name,
                FacebookUrl = member.FacebookUrl,
                InstagramUrl = member.InstagramUrl,
                LinkedinUrl = member.LinkedinUrl,
                Image = member.Image,
                Description = member.Description
            };
            return membersDTO;
        }

        public Member FromMembersDTOtoMember(MemberInsertDTO membersDTO)
        {
            return new Member()
            {
                Name = membersDTO.Name,
                FacebookUrl = membersDTO.FacebookUrl,
                InstagramUrl = membersDTO.InstagramUrl,
                LinkedinUrl = membersDTO.LinkedinUrl,
                Image = membersDTO.Image.ToString(),
                Description = membersDTO.Description
            };
        }

        #endregion Member Mapper

        #region Contact Mappers

        public ContactDTO FromContactsToContactsDto(Contacts contact)
        {
            var contactDTO = new ContactDTO()
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Message = contact.Message
            };
            return contactDTO;
        }

        #endregion Contact Mappers

        #region Organization Mappers

        public OrganizationsDTO FromOrganizationToOrganizationDto(Organizations organization)
        {
            return new OrganizationsDTO
            {
                Name = organization.Name,
                Image = organization.Image,
                Phone = organization.Phone,
                Address = organization.Address
            };
        }

        #endregion Organization Mappers

        #region User Mappers

        public User FromUserRegistrationDtoToUser(UserRegistrationDTO user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = Encrypt.GetSHA256(user.Password)
            };
        }

        public UserRegistrationDTO FromUserToUserRegistrationDto(User user)
        {
            return new UserRegistrationDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = Encrypt.GetSHA256(user.Password)
            };
        }

        #endregion User Mappers

        #region Activities Mappers

        public ActivitiesDTO FromActivitiesToActivitiesDTO(Activities activities)
        {
            var activitiesDTO = new ActivitiesDTO()
            {
                Name = activities.Name,
                Content = activities.Content,
                Image = activities.Image
            };
            return activitiesDTO;
        }

        public Activities FromActivitiesDTOToActivities(ActivitiesDTO activitiesDTO)
        {
            var activities = new Activities()
            {
                Name = activitiesDTO.Name,
                Content = activitiesDTO.Content,
                Image = activitiesDTO.Image
            };
            return activities;
        }

        #endregion Activities Mappers
    }
}