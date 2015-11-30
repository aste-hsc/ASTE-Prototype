package fi.aste.common;

import fi.aste.constant.ModuleStatus;

/**
 * Created with IntelliJ IDEA.
 * User: vmm
 * Date: 27.11.2015
 * Time: 22:41
 * Copyright 2015 Oy Aurora Data and Systems Ltd.
 */
public class Ping {
    private ModuleStatus status = null;
    private String message = null;

    public Ping(ModuleStatus status, String message) {
        this.status = status;
        this.message = message;
    }

    public ModuleStatus getStatus() {
        return status;
    }

    public String getMessage() {
        return message;
    }
}
