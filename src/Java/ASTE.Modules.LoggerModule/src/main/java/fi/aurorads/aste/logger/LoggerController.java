package fi.aurorads.aste.logger;

import fi.aste.common.Config;
import fi.aste.common.MethodDescription;
import fi.aste.common.Ping;
import fi.aste.constant.ModuleStatus;
import fi.aste.constant.Type;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletResponse;
import java.util.Date;
import java.util.concurrent.atomic.AtomicLong;

import static org.springframework.web.bind.annotation.RequestMethod.GET;
import static org.springframework.web.bind.annotation.RequestMethod.PUT;

/**
 * Created with IntelliJ IDEA.
 * User: vmm
 * Date: 27.11.2015
 * Time: 22:33
 * Copyright 2015 Oy Aurora Data and Systems Ltd.
 */
@RestController
public class LoggerController {

    private static final Logger log = LoggerFactory.getLogger(LoggerController.class);

    private static final Config config = new Config(
            "LoggerModule",
            Type.MODULE,
            new String[] {
                    "946B0E33-0A24-441F-A681-D2924245E7A2"
            },
            "Simple logger module concept",
            new MethodDescription[] {
                    new MethodDescription("log", "source: client system's name, message: log content, eventTimestamp: " +
                            "timestamp of event being logged")
            },
            "1.0",
            "Oy Aurora Data and Systems Ltd.",
            "www.aurorads.fi",
            "58965B8C-9295-43B1-8CBA-31003E565003"
    );

    private final AtomicLong counter = new AtomicLong();

    @RequestMapping(path = "/1.0/ping", method = GET)
    public Ping ping() {
        try {
            log.info("Replying to ping request");
            return new Ping(ModuleStatus.SUCCESS, "LoggerModule running");
        } catch (Throwable t) {
            return new Ping(ModuleStatus.ERROR, "Logging not successful. Exception message: " + t.getMessage());
        }
    }

    @RequestMapping(path = "/1.0/config", method = GET)
    public Config config() {
        return config;
    }

    @RequestMapping(path = "/1.0/log", method = PUT)
    public String log(@RequestBody LoggerModel loggerModel,
                      HttpServletResponse response) {
        long l = counter.incrementAndGet();
        log.info("Log id {}: at {} {} wrote to log {} ", l, loggerModel.getEventTimestamp(),
                loggerModel.getSource(), loggerModel.getMessage());
        response.setStatus(HttpServletResponse.SC_CREATED);
        return "{\"log_id\": " + l + "}";
    }
}
