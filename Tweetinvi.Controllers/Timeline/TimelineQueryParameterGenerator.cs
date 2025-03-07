﻿using System;
using Tweetinvi.Controllers.Properties;
using Tweetinvi.Core.Injectinvi;
using Tweetinvi.Core.Interfaces.Models;
using Tweetinvi.Core.Interfaces.Parameters;
using Tweetinvi.Core.Interfaces.Parameters.QueryParameters;

namespace Tweetinvi.Controllers.Timeline
{
    public interface ITimelineQueryParameterGenerator
    {
        string GenerateExcludeRepliesParameter(bool excludeReplies);
        string GenerateIncludeContributorDetailsParameter(bool includeContributorDetails);
        string GenerateIncludeRTSParameter(bool includeRTS);
        string GenerateIncludeUserEntitiesParameter(bool includeUserEntities);

        IHomeTimelineParameters CreateHomeTimelineParameters();
        IUserTimelineParameters CreateUserTimelineParameters();
        IMentionsTimelineParameters CreateMentionsTimelineParameters();
        IRetweetsOfMeTimelineRequestParameters CreateRetweetsOfMeTimelineParameters();

        IUserTimelineQueryParameters CreateUserTimelineQueryParameters(
            IUserIdentifier userIdentifier,
            IUserTimelineParameters userTimelineParameters);
    }

    public class TimelineQueryParameterGenerator : ITimelineQueryParameterGenerator
    {
        private readonly IFactory<IHomeTimelineParameters> _homeTimelineRequestParameterFactory;
        private readonly IFactory<IMentionsTimelineParameters> _mentionsTimelineRequestParameterFactory;
        private readonly IFactory<IRetweetsOfMeTimelineRequestParameters> _retweetsOfMeTimelineRequestParameterFactory;
        private readonly IFactory<IUserTimelineParameters> _userTimelineRequestParameterFactory;
        private readonly IFactory<IUserTimelineQueryParameters> _userTimelineRequestQueryParameterFactory;

        public TimelineQueryParameterGenerator(
            IFactory<IHomeTimelineParameters> homeTimelineRequestParameterFactory,
            IFactory<IMentionsTimelineParameters> mentionsTimelineRequestParameterFactory,
            IFactory<IRetweetsOfMeTimelineRequestParameters> retweetsOfMeTimelineRequestParameterFactory,
            IFactory<IUserTimelineParameters> userTimelineRequestParameterFactory, 
            IFactory<IUserTimelineQueryParameters> userTimelineRequestQueryParameterFactory)
        {
            _homeTimelineRequestParameterFactory = homeTimelineRequestParameterFactory;
            _mentionsTimelineRequestParameterFactory = mentionsTimelineRequestParameterFactory;
            _retweetsOfMeTimelineRequestParameterFactory = retweetsOfMeTimelineRequestParameterFactory;
            _userTimelineRequestParameterFactory = userTimelineRequestParameterFactory;
            _userTimelineRequestQueryParameterFactory = userTimelineRequestQueryParameterFactory;
        }

        public string GenerateExcludeRepliesParameter(bool excludeReplies)
        {
            return String.Format(Resources.TimelineParameter_ExcludeReplies, excludeReplies);
        }

        public string GenerateIncludeContributorDetailsParameter(bool includeContributorDetails)
        {
            return String.Format(Resources.TimelineParameter_IncludeContributorDetails, includeContributorDetails);
        }

        public string GenerateIncludeRTSParameter(bool includeRTS)
        {
            return String.Format(Resources.QueryParameter_IncludeRetweets, includeRTS);
        }

        public string GenerateIncludeUserEntitiesParameter(bool includeUserEntities)
        {
            return string.Format(Resources.TimelineParameter_IncludeUserEntities, includeUserEntities);
        }

        // User Parameters
        public IHomeTimelineParameters CreateHomeTimelineParameters()
        {
            return _homeTimelineRequestParameterFactory.Create();
        }

        public IUserTimelineParameters CreateUserTimelineParameters()
        {
            return _userTimelineRequestParameterFactory.Create();
        }

        public IMentionsTimelineParameters CreateMentionsTimelineParameters()
        {
            return _mentionsTimelineRequestParameterFactory.Create();
        }

        public IRetweetsOfMeTimelineRequestParameters CreateRetweetsOfMeTimelineParameters()
        {
            return _retweetsOfMeTimelineRequestParameterFactory.Create();
        }

        // Query Parameters
        public IUserTimelineQueryParameters CreateUserTimelineQueryParameters(IUserIdentifier userIdentifier, IUserTimelineParameters userTimelineParameters)
        {
            var userIdentifierParameter = TweetinviFactory.CreateConstructorParameter("userIdentifier", userIdentifier);
            var queryParameters = TweetinviFactory.CreateConstructorParameter("parameters", userTimelineParameters);

            return _userTimelineRequestQueryParameterFactory.Create(userIdentifierParameter, queryParameters);
        }
    }
}