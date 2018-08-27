using Interfaces.Data.Version1;
using Logic;
using PipServices.Commons.Commands;
using PipServices.Commons.Data;
using PipServices.Commons.Validate;
using System;

namespace Services
{
    public class BeaconsCommandSet: CommandSet
    {
        private IBeaconsController _Controller;

        public BeaconsCommandSet(IBeaconsController Controller)
        {
            _Controller = Controller;

            AddCommand(MakeGetBeaconsCommand());
            AddCommand(MakeGetOneByIdBeaconsCommand());
            AddCommand(MakeGetBeaconByUdiCommand());
            AddCommand(MakeCalculatePositionCommand());
            AddCommand(MakeCreateBeaconCommand());
            AddCommand(MakeUpdateBeaconCommand());
            AddCommand(MakeDeleteBeaconByIdCommand());
        }

        private ICommand MakeGetBeaconsCommand()
        {
            return new Command(
                "get_beacons",
                new ObjectSchema()
                    .WithOptionalProperty("filter", new FilterParamsSchema())
                    .WithOptionalProperty("paging", new PagingParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var filter = FilterParams.FromValue(parameters.Get("filter"));
                    var paging = PagingParams.FromValue(parameters.Get("paging"));
                    return await _Controller.GetPageByFilterAsync(correlationId, filter, paging);
                });
        }

        private ICommand MakeGetOneByIdBeaconsCommand()
        {
            return new Command(
                "get_beacon",
                new ObjectSchema()
                    .WithRequiredProperty("id", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var id = parameters.GetAsString("id");
                    return await _Controller.GetOneByIdAsync(correlationId, id);
                });
        }

        private ICommand MakeGetBeaconByUdiCommand()
        {
            return new Command(
                "get_beacon_by_udi",
                new ObjectSchema()
                    .WithRequiredProperty("udi", TypeCode.String),
                async (correlationId, parameters) =>
                {
                    var udi = parameters.GetAsString("udi");
                    return await _Controller.GetOneByUdiAsync(correlationId, udi);
                });
        }

        private ICommand MakeCalculatePositionCommand()
        {
            return new Command(
                "calculate_position",
                new ObjectSchema()
                    .WithRequiredProperty("site_id", new FilterParamsSchema())
                    .WithRequiredProperty("udis", new PagingParamsSchema()),
                async (correlationId, parameters) =>
                {
                    var siteId = parameters.GetAsString("site_id");
                    string udis = parameters.GetAsString("udis");
                    return await _Controller.CalculatePosition(correlationId, siteId, new []{ udis });
                });
        }

        private ICommand MakeCreateBeaconCommand()
        {
            return new Command(
                "create_beacon",
                new ObjectSchema()
                    .WithOptionalProperty("beacon", new BeaconV1Schema()),
                async (correlationId, parameters) =>
                {
                    var beacon = (BeaconV1)parameters.Get("beacon");
                    return await _Controller.CreateAsync(correlationId, beacon);
                });
        }

        private ICommand MakeUpdateBeaconCommand()
        {
            return new Command(
               "update_beacon",
               new ObjectSchema()
                   .WithOptionalProperty("beacon", new BeaconV1Schema()),
               async (correlationId, parameters) =>
               {
                   var beacon = (BeaconV1)parameters.Get("beacon");
                   return await _Controller.UpdateAsync(correlationId, beacon);
               });
        }

        private ICommand MakeDeleteBeaconByIdCommand()
        {
            return new Command(
               "delete_beacon",
               new ObjectSchema()
                   .WithRequiredProperty("id", TypeCode.String),
               async (correlationId, parameters) =>
               {
                   var id = parameters.GetAsString("id");
                   return await _Controller.DeleteByIdAsync(correlationId, id);
               });
        }

    }
}
