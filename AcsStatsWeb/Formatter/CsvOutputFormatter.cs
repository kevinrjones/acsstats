using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsTypes.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace AcsStatsWeb.Formatter;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type type)
    {
        if (typeof(Envelope<IReadOnlyList<IndividualBattingDetailsDto>>).IsAssignableFrom(type)
            || typeof(Envelope<IReadOnlyList<BattingCareerRecordDto>>).IsAssignableFrom(type)
            || typeof(Envelope<IReadOnlyList<IndividualBowlingDetailsDto>>).IsAssignableFrom(type)
            || typeof(Envelope<IReadOnlyList<BowlingCareerRecordDto>>).IsAssignableFrom(type)
            || typeof(Envelope<IReadOnlyList<IndividualBowlingDetailsDto>>).IsAssignableFrom(type)
            || typeof(Envelope<IReadOnlyList<FieldingCareerRecordDto>>).IsAssignableFrom(type)
           )
        {
            return base.CanWriteType(type);
        }

        return false;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();

        if (context.Object is Envelope<IReadOnlyList<BattingCareerRecordDto>> playerBattingEnv)
        {
            buffer.AppendLine(playerBattingEnv.Result.FormatHeader());

            foreach (var dto in playerBattingEnv.Result)
            {
                buffer.AppendLine(dto.Format());
            }
        }
        else if (context.Object is Envelope<IReadOnlyList<IndividualBattingDetailsDto>> indBattingEnv)
        {
            buffer.AppendLine(indBattingEnv.Result.FormatHeader());

            foreach (var dto in indBattingEnv.Result)
            {
                buffer.AppendLine(dto.Format());
            }
        }
        else if (context.Object is Envelope<IReadOnlyList<BowlingCareerRecordDto>> playerBowlingEnv)
        {
            buffer.AppendLine(playerBowlingEnv.Result.FormatHeader());

            foreach (var dto in playerBowlingEnv.Result)
            {
                buffer.AppendLine(dto.Format());
            }
        }
        else if (context.Object is Envelope<IReadOnlyList<IndividualBowlingDetailsDto>> indBowlingEnv)
        {
            buffer.AppendLine(indBowlingEnv.Result.FormatHeader());

            foreach (var dto in indBowlingEnv.Result)
            {
                buffer.AppendLine(dto.Format());
            }
        }
        else if (context.Object is Envelope<IReadOnlyList<FieldingCareerRecordDto>> playerFieldingEnv)
        {
            buffer.AppendLine(playerFieldingEnv.Result.FormatHeader());

            foreach (var dto in playerFieldingEnv.Result)
            {
                buffer.AppendLine(dto.Format());
            }
        }
        else if (context.Object is Envelope<IReadOnlyList<IndividualFieldingDetailsDto>> indFieldingEnv)
        {
            buffer.AppendLine(indFieldingEnv.Result.FormatHeader());

            foreach (var dto in indFieldingEnv.Result)
            {
                buffer.AppendLine(dto.Format());
            }
        }


        await using var writer = context.WriterFactory(response.Body, selectedEncoding);
        await writer.WriteAsync(buffer.ToString());
    }


}