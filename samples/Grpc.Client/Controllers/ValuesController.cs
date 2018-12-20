﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Extension.Client;
using Grpc.Extension.Client.LoadBalance;
using Microsoft.AspNetCore.Mvc;

namespace Grpc.Client.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private ChannelFactory ChannelFactory { get; }

		private GrpcLoadBalance GrpcLoadBalance { get; }

		private ClientFactory ClientFactory { get; }

		public ValuesController(ChannelFactory channelFactory, GrpcLoadBalance grpcLoadBalance, ClientFactory clientFactory)
		{
			ChannelFactory = channelFactory;
			GrpcLoadBalance = grpcLoadBalance;
			ClientFactory = clientFactory;
		}

		// GET api/values
		[HttpGet]
		public IActionResult Get()
		{
			//var channel = new Channel("192.168.1.129:50051", ChannelCredentials.Insecure);
			//var client = new Hello.HelloClient(channel);
			//return Ok(new
			//{
			//	gRpc = client.SayHello(new HelloRequest
			//	{
			//		Name = "Owen"
			//	}),
			//	services = ChannelFactory.GetChannels("grpc-server1")
			//});
			//var channel = GrpcLoadBalance.GetService("grpc-server");
			var ch = new Channel("192.168.1.129", 50054, ChannelCredentials.Insecure);
			var client = new Hello.HelloClient(ch);

			//var client = ClientFactory.Get<Hello.HelloClient>("grpc-server");
			return Ok(client.SayHello(new HelloRequest
			{
				Name = "Owen"
			}));
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
