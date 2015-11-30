package fi.aurorads.aste.logger;

import com.fasterxml.jackson.databind.annotation.JsonDeserialize;
import fi.aste.util.CustomDateDeserializer;

import java.util.Date;

/**
 * Created with IntelliJ IDEA.
 * User: vmm
 * Date: 30.11.2015
 * Time: 13:46
 * Copyright 2015 Oy Aurora Data and Systems Ltd.
 */
public class LoggerModel {

    private String source = null;
    private String message = null;
    private Date eventTimestamp = null;

    public LoggerModel() {
    }

    public String getSource() {
        return source;
    }

    public void setSource(String source) {
        this.source = source;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public Date getEventTimestamp() {
        return eventTimestamp;
    }

    @JsonDeserialize(using = CustomDateDeserializer.class)
    public void setEventTimestamp(Date eventTimestamp) {
        this.eventTimestamp = eventTimestamp;
    }
}
