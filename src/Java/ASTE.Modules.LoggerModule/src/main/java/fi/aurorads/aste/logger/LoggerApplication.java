package fi.aurorads.aste.logger;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.builder.SpringApplicationBuilder;
import org.springframework.boot.context.web.SpringBootServletInitializer;

/**
 * Created with IntelliJ IDEA.
 * User: vmm
 * Date: 27.11.2015
 * Time: 22:51
 * Copyright 2015 Oy Aurora Data and Systems Ltd.
 */
@SpringBootApplication
public class LoggerApplication extends SpringBootServletInitializer {

    public static void main(String[] args) {
        SpringApplication.run(LoggerApplication.class, args);
    }

    @Override
    protected SpringApplicationBuilder configure(SpringApplicationBuilder builder) {
        return builder.sources(LoggerApplication.class);
    }
}
