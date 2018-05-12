﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using DAL;
using DAL.Models;

namespace BLL
{
    public class PersonService
    {
        private readonly PersonsAccessInterface _personAccess;
        private readonly GenericAccessInterface _genericAccess;
        private readonly FollowingsAccessInterface _followingsAccess;
        private Regex email_regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public PersonService(PersonsAccessInterface personAccess, GenericAccessInterface genericAccess, FollowingsAccessInterface followingsAccess)
        {
            _genericAccess = genericAccess;
            _personAccess = personAccess;
            _followingsAccess = followingsAccess;
        }

        public POCO.Person Create(POCO.Person person)
        {
            if(person == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(person.Email) || string.IsNullOrEmpty(person.Password) ||
                string.IsNullOrEmpty(person.Name) )
            {
                throw new ArgumentOutOfRangeException();
            }

            // check email reg express
            if (!checkRegularExpression(person.Email, email_regex))
            {
                throw new InvalidEnumArgumentException();
            }

            var p = new Persons
            {
                Name = person.Name,
                Email = person.Email,
                Password = person.Password,
                Age = person.Age,
                Sex = person.Sex,
                Profile = person.Profile,
                Image = person.Image
            };
            _genericAccess.Add(p);
            person.Id = p.Id;
            return person;
        }

        public POCO.Person getPersonById (int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            var p = _genericAccess.GetById<Persons>(id);
            var person = EntityObjToPOCO(p);
            return person;
        }

        public POCO.Person login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentOutOfRangeException();
            }

            // check email reg express
            if (!checkRegularExpression(email, email_regex))
            {
                throw new InvalidEnumArgumentException();
            }

            var p = _personAccess.Login(email, password);
            var person = EntityObjToPOCO(p);
            return person;
        }

        public bool update(POCO.Person person)
        {
            if(person == null)
            {
                throw new ArgumentNullException();
            }

            var entity = POCOObjToEntity(person);
            entity = _genericAccess.Update<Persons>(entity, person.Id);
            if(entity != null)
            {
                return true;
            }
            return false;
        }

        public bool delete (int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            return _genericAccess.Delete<Persons>(id);
        }

        public List<POCO.Person> getFollowings(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            var f_list = _followingsAccess.GetFollowings(id);
            var result_list = new List<POCO.Person>();
            foreach(Followings record in f_list)
            {
                var personInfo = EntityObjToPOCO(record.FollowingNavigation);
                result_list.Add(personInfo);
            }
            return result_list;
        }

        private bool checkRegularExpression(String input, Regex expression)
        {
            var match = expression.Match(input);
            if(match.Success)
            {
                return true;
            }
            return false;
        }

        public static POCO.Person EntityObjToPOCO(Persons entity)
        {
            if (entity == null)
                return null;

            var person = new POCO.Person
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Password = entity.Password,
                Age = entity.Age,
                Sex = entity.Sex,
                Profile = entity.Profile,
                Image = entity.Image
            };

            return person;
        }

        public static Persons POCOObjToEntity(POCO.Person person)
        {
            if(person == null)
            {
                return null;
            }

            var p = new Persons
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Password = person.Password,
                Age = person.Age,
                Sex = person.Sex,
                Profile = person.Profile,
                Image = person.Image
            };
            return p;
        }
    }
}
