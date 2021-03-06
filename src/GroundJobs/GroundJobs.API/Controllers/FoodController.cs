﻿using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using GroundJobs.Services;
using GroundJobs.Services.FoodServices;

namespace GroundJobs.API.Controllers
{
    [Route("api/[controller]")]
    public class FoodController : ApiController
    {
        // GET api/food/ls73nu
        [Route("api/food/{postcode}")]
        public EateryResult Get(string postcode)
        {
            return ServiceBus.ServiceBus.Instance.Aggregate<ClosestEateryRequest, ClosestEateryResponse, EateryResult>(new ClosestEateryRequest {Command = new PostCodeSearchCommand {Postcode = postcode}});
        }

        // GET api/food/ls73nu
        [Route("api/food/{postcode}/all"), HttpGet]
        public IEnumerable<EateryResult> All(string postcode)
        {
            return ServiceBus.ServiceBus.Instance.Aggregate<ClosestEateryRequest, ClosestEateryResponse, IEnumerable<EateryResult>>(new ClosestEateryRequest { Command = new PostCodeSearchCommand { Postcode = postcode } });
        }
    }
}