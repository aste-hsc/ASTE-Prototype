package fi.aste.common;

import fi.aste.constant.Type;

/**
 * Created with IntelliJ IDEA.
 * User: vmm
 * Date: 27.11.2015
 * Time: 23:05
 * Copyright 2015 Oy Aurora Data and Systems Ltd.
 */
public class Config {
    private String name = null;
    private Type type = null;
    private String[] dependencies = null;
    private String description = null;
    private MethodDescription[] methods = null;
    private String version = null;
    private String author = null;
    private String authorContact = null;
    private String guid = null;

    public Config(String name, Type type, String[] dependencies, String description, MethodDescription[] methods,
                  String version, String author, String authorContact, String guid) {
        this.name = name;
        this.type = type;
        this.dependencies = dependencies;
        this.description = description;
        this.methods = methods;
        this.version = version;
        this.author = author;
        this.authorContact = authorContact;
        this.guid = guid;
    }

    public String getName() {
        return name;
    }

    public Type getType() {
        return type;
    }

    public String[] getDependencies() {
        return dependencies;
    }

    public String getDescription() {
        return description;
    }

    public MethodDescription[] getMethods() {
        return methods;
    }

    public String getVersion() {
        return version;
    }

    public String getAuthor() {
        return author;
    }

    public String getAuthorContact() {
        return authorContact;
    }

    public String getGuid() {
        return guid;
    }
}
